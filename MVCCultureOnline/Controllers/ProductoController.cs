using CultureOnline.Application.DTOs;
using CultureOnline.Application.Services.Implementations;
using CultureOnline.Application.Services.Interfaces;
using CultureOnline.Infraestructure.Data;
using CultureOnline.Infraestructure.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCultureOnline.Views.ViewModels;

namespace MVCCultureOnline.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IServiceProducto _serviceProducto;
        private readonly IServiceCategoria _serviceCategoria;
        private readonly CultureOnlineContext _context;

        public ProductoController(IServiceProducto service, IServiceCategoria serviceCategoria, CultureOnlineContext context)
        {
            _serviceProducto = service;
            _serviceCategoria = serviceCategoria;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var productos = await _serviceProducto.ListAsync();
            var categorias = await _serviceCategoria.ListAsync();

            ViewBag.ListCategorias = categorias;
            return View(productos);
        }

        [HttpGet]
        public async Task<IActionResult> Detalle(int id)
        {
            var producto = await _serviceProducto.FindByIdAsync(id);

            if (producto == null)
                return NotFound();

            producto.Reseñas = await _serviceProducto.GetReseñasPorProductoAsync(id);

            return View("Detalle", producto);
        }

        /*[HttpGet]
        public async Task<IActionResult> BuscarPorCategoria(int categoriaId)
        {
            IEnumerable<ProductoDTO> productos;

            if (categoriaId == 0)
                productos = await _serviceProducto.ListAsync();
            else
                productos = await _serviceProducto.GetProductoByCategoria(categoriaId);

            return PartialView("_ListProductos", productos);
        }*/
        public async Task<IActionResult> Promociones()
        {
            var productos = await _serviceProducto.ListAsync();
            // Solo productos con precio promocional válido (calculado en el servicio)
            var productosEnPromocion = productos
                .Where(p => p.PrecioPromocional.HasValue && p.PrecioPromocional.Value < p.Precio)
                .ToList();
            return View(productosEnPromocion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ViewModelProducto model)
        {
            // 1. Imagen por defecto
            var imagenesGuardadas = new List<string>();

            // Valida que haya al menos una categoría seleccionada
            if (model.CategoriaIds == null || !model.CategoriaIds.Any())
            {
                ModelState.AddModelError(nameof(model.CategoriaIds), "Debe seleccionar al menos una categoría.");
            }

            // Valida que haya al menos una etiqueta seleccionada
            if (model.EtiquetaIds == null || !model.EtiquetaIds.Any())
            {
                ModelState.AddModelError(nameof(model.EtiquetaIds), "Debe seleccionar al menos una etiqueta.");
            }

            // Verifica si el modelo no tiene imágenes o la colección está vacía
            if (model.Imagenes == null || !model.Imagenes.Any())
            {
                // Si no hay imágenes, agrega una imagen por defecto
                imagenesGuardadas.Add("/images/default.jpg");
            }
            else
            {
                // Recorre todas las imágenes del modelo
                foreach (var nombre in model.Imagenes)
                {
                    // Valida que el nombre de la imagen no esté vacío o sea solo espacios
                    if (!string.IsNullOrWhiteSpace(nombre))
                    {
                        // Agrega la imagen a la lista de imágenes guardadas
                        imagenesGuardadas.Add(nombre);
                    }
                }
            }

            // Validar TipoProducto (string no nulo ni vacío)
            if (string.IsNullOrWhiteSpace(model.TipoProducto))
            {
                ModelState.AddModelError(nameof(model.TipoProducto), "Debe seleccionar el tipo de producto.");
            }

            // Validar Imagenes (lista no vacía y con elementos válidos)
            if (model.Imagenes == null || !model.Imagenes.Any() || model.Imagenes.All(i => string.IsNullOrWhiteSpace(i)))
            {
                ModelState.AddModelError(nameof(model.Imagenes), "Debe cargar al menos una imagen.");
            }


            // Validaciones también
            if (!ModelState.IsValid)
            {
                model.Categorias = _context.Categorias
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Nombre })
                    .ToList();

                model.Etiquetas = _context.Etiquetas
                    .Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Descripcion })
                    .ToList();

                model.Autores = _context.Autor
                    .Select(a => new SelectListItem { Value = a.IdAutor.ToString(), Text = a.Descripcion })
                    .ToList();

                ViewBag.NotificationMessage = "Errores en el formulario";
                return View(model);
            }

            //  Agregar el "+" a la clasificación de edad
            if (int.TryParse(model.ClasificacionEdad, out int edad))
            {
                model.ClasificacionEdad = $"{edad}+";
            }

            // Crear el producto
            var producto = new Productos
            {
                Nombre = model.Nombre,
                Descripcion = model.Descripcion,
                Precio = model.Precio,
                PromedioValoracion = 0,
                Estado = "Activo",
                Stock = (int)model.Stock,
                EsPersonalizable = model.EsPersonalizable,
            };
            if (model.TipoProducto == "Libro")
            {
                producto.AnioPublicacion = model.AnioPublicacion;
                producto.IdAutor = model.AutorId;
                producto.ClasificacionEdad = model.ClasificacionEdad;
                producto.Editorial = model.Editorial;
            }

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();



            // Asociar Categorías
            if (model.CategoriaIds != null && model.CategoriaIds.Any())
            {
                foreach (var idCategoria in model.CategoriaIds)
                {
                    var prodCat = new ProductoCategorias
                    {
                        ProductoId = producto.Id,
                        CategoriaId = idCategoria
                    };

                    _context.ProductoCategorias.Add(prodCat);
                }
            }

            // Asociar Etiquetas
            if (model.EtiquetaIds != null && model.EtiquetaIds.Any())
            {
                foreach (var idEtiqueta in model.EtiquetaIds)
                {
                    _context.ProductoEtiquetas.Add(new ProductoEtiquetas
                    {
                        ProductoId = producto.Id,
                        EtiquetaId = idEtiqueta
                    });
                }
            }

            // Asociar Imágenes
            foreach (var ruta in imagenesGuardadas)
            {
                _context.ProductoImagenes.Add(new ProductoImagenes
                {
                    ProductoId = producto.Id,
                    Ruta = ruta
                });
            }
            Console.WriteLine("Producto ID luego de guardar: " + producto.Id);
            Console.WriteLine("Imágenes a guardar: " + imagenesGuardadas.Count);
            Console.WriteLine("Categorías a guardar: " + model.CategoriaIds?.Count);
            Console.WriteLine("Etiquetas a guardar: " + model.EtiquetaIds?.Count);
            await _context.SaveChangesAsync();

            TempData["MensajeExito"] = $"Producto '{producto.Nombre}' creado correctamente.";
            return RedirectToAction("Create", new { id = producto.Id });
            //return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new ViewModelProducto();

            // Cargar listas para dropdowns
            model.Categorias = _context.Categorias
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Nombre })
                .ToList();

            model.Etiquetas = _context.Etiquetas
                .Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Descripcion })
                .ToList();

            model.Autores = _context.Autor
                .Select(a => new SelectListItem { Value = a.IdAutor.ToString(), Text = a.Descripcion })
                .ToList();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ReseñasParcial(int productoId)
        {
            var producto = await _serviceProducto.FindByIdAsync(productoId);
            if (producto == null)
                return NotFound();

            return PartialView("_ReseñasProducto", producto.Reseñas.ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // Obtener el producto
            var producto = await _serviceProducto.FindByIdAsync(id);
            if (producto == null)
                return NotFound(); // Devuelve 404 si el producto no existe

            //Mapear a ViewModel
            var model = new ViewModelProducto
            {
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                EsPersonalizable = producto.EsPersonalizable,
                // Lógica para determinar tipo de producto

                // Propiedades específicas de libros
                TipoProducto = producto.AnioPublicacion.HasValue ? "Libro" : "Marcapáginas",
                AnioPublicacion = producto.AnioPublicacion,
                ClasificacionEdad = string.IsNullOrWhiteSpace(producto.ClasificacionEdad) ? null: producto.ClasificacionEdad.Replace("+", ""),
                Editorial = producto.Editorial,
                AutorId = producto.IdAutor,

                // Colecciones relacionadas
                Imagenes = _context.ProductoImagenes
                    .Where(pi => pi.ProductoId == producto.Id)
                    .Select(pi => pi.Ruta).ToList(),
                CategoriaIds = _context.ProductoCategorias
                    .Where(pc => pc.ProductoId == producto.Id)
                    .Select(pc => pc.CategoriaId).ToList(),
                EtiquetaIds = _context.ProductoEtiquetas
                    .Where(pe => pe.ProductoId == producto.Id)
                    .Select(pe => pe.EtiquetaId).ToList()
            };

            // Cargar listas desplegables
            model.Categorias = _context.Categorias
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Nombre })
                .ToList();

            model.Etiquetas = _context.Etiquetas
                .Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Descripcion })
                .ToList();

            model.Autores = _context.Autor
                .Select(a => new SelectListItem { Value = a.IdAutor.ToString(), Text = a.Descripcion })
                .ToList();

            // Lista fija de tipos de producto
            model.TiposProducto = new List<SelectListItem>
                {
                new SelectListItem { Value = "Libro", Text = "Libro" },
                new SelectListItem { Value = "Marcapáginas", Text = "Marcapáginas" }
                };

            // Calcular el promedio de valoraciones promedio
            var valoraciones = await _context.Reseñas
                .Where(v => v.ProductoId == producto.Id)
                .ToListAsync();
            model.PromedioValoracion = valoraciones.Any()
   ? (float)valoraciones.Average(r => r.Valoracion.Value)
   : 0.0f;
            return View("Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ViewModelProducto model, List<IFormFile> ImagenesNuevas, List<string> ImagenesEliminadas)
        {
            if (!ModelState.IsValid)
            {
                // Recargar listas desplegables si hay errores
                model.Categorias = _context.Categorias
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Nombre }).ToList();
                model.Etiquetas = _context.Etiquetas
                    .Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Descripcion }).ToList();
                model.Autores = _context.Autor
                    .Select(a => new SelectListItem { Value = a.IdAutor.ToString(), Text = a.Descripcion }).ToList();

                model.TiposProducto = new List<SelectListItem>
                     {
                      new SelectListItem { Value = "Libro", Text = "Libro" },
                      new SelectListItem { Value = "Marcapáginas", Text = "Marcapáginas" }
                     };


                return View("Edit", model);
            }

            //Obtener el producto existente
            var producto = await _context.Productos.FindAsync(model.Id);
            if (producto == null)
                return NotFound();

            // Actualizar propiedades
            producto.Nombre = model.Nombre;
            producto.Descripcion = model.Descripcion;
            producto.Precio = model.Precio;
            producto.Stock = (int)model.Stock;
            producto.EsPersonalizable = model.EsPersonalizable;

            if (model.TipoProducto == "Libro")
            {
                // Aqui se asegura que ClasificaciónEdad tenga el "+" si es un número
                if (!string.IsNullOrWhiteSpace(model.ClasificacionEdad) && int.TryParse(model.ClasificacionEdad, out int edadEdit))
                {
                    model.ClasificacionEdad = $"{edadEdit}+";
                }
                producto.AnioPublicacion = model.AnioPublicacion;
                producto.ClasificacionEdad = model.ClasificacionEdad;
                producto.Editorial = model.Editorial;
                producto.IdAutor = model.AutorId;
            }
            else
            {
                producto.AnioPublicacion = null;
                producto.ClasificacionEdad = null;
                producto.Editorial = null;
                producto.IdAutor = null;
            }

                // Actualizar categorías
                var categoriasActuales = _context.ProductoCategorias.Where(pc => pc.ProductoId == producto.Id);
                _context.ProductoCategorias.RemoveRange(categoriasActuales);

                if (model.CategoriaIds != null)
                {
                    foreach (var catId in model.CategoriaIds)
                    {
                        _context.ProductoCategorias.Add(new ProductoCategorias
                        {
                            ProductoId = producto.Id,
                            CategoriaId = catId
                        });
                    }
                }

                // Actualizar etiquetas
                var etiquetasActuales = _context.ProductoEtiquetas.Where(pe => pe.ProductoId == producto.Id);
                _context.ProductoEtiquetas.RemoveRange(etiquetasActuales);

                if (model.EtiquetaIds != null)
                {
                    foreach (var etiqId in model.EtiquetaIds)
                    {
                        _context.ProductoEtiquetas.Add(new ProductoEtiquetas
                        {
                            ProductoId = producto.Id,
                            EtiquetaId = etiqId
                        });
                    }
                }

                // Actualizar imágenes
                // Elimina imágenes que el usuario quitó
                if (ImagenesEliminadas != null)
                {
                    foreach (var ruta in ImagenesEliminadas)
                    {
                        var nombreArchivo = Path.GetFileName(ruta);
                        var imagen = await _context.ProductoImagenes
                            .FirstOrDefaultAsync(pi => pi.ProductoId == producto.Id && pi.Ruta.Contains(nombreArchivo));

                        if (imagen != null)
                        {
                            _context.ProductoImagenes.Remove(imagen);

                            var rutaFisica = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", nombreArchivo);
                            if (System.IO.File.Exists(rutaFisica))
                                System.IO.File.Delete(rutaFisica);
                        }
                    }
                }

                //  Agregar nuevas imágenes
                if (ImagenesNuevas != null && ImagenesNuevas.Count > 0)
                {
                    foreach (var archivo in ImagenesNuevas)
                    {
                        // Obtener el nombre original del archivo
                        var nombreArchivo = Path.GetFileName(archivo.FileName);
                        var rutaBase = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

                        // Verificar si el archivo ya existe y agregar un sufijo numérico
                        var nombreFinal = nombreArchivo;
                        var contador = 1;
                        while (System.IO.File.Exists(Path.Combine(rutaBase, nombreFinal)))
                        {
                            nombreFinal = $"{Path.GetFileNameWithoutExtension(nombreArchivo)}_{contador}{Path.GetExtension(nombreArchivo)}";
                            contador++;
                        }

                        Directory.CreateDirectory(rutaBase);
                        var rutaGuardado = Path.Combine(rutaBase, nombreFinal);

                        using (var stream = new FileStream(rutaGuardado, FileMode.Create))
                        {
                            await archivo.CopyToAsync(stream);
                        }

                        _context.ProductoImagenes.Add(new ProductoImagenes
                        {
                            ProductoId = producto.Id,
                            Ruta = nombreFinal // Guarda el nombre original (o con sufijo si hubo duplicados)
                        });
                    }
                }
            //Mensaje
            await _context.SaveChangesAsync();
            TempData["MensajeExito"] = "Producto editado correctamente";
            return RedirectToAction("Edit", new { id = producto.Id });
        }
        

        [HttpGet]
        public async Task<IActionResult> DetalleParcial(int id)
        {
            var producto = await _serviceProducto.FindByIdAsync(id);
            if (producto == null)
                return NotFound();
            producto.Reseñas = await _serviceProducto.GetReseñasPorProductoAsync(id);
            return PartialView("_DetalleProducto", producto);
        }
    }

}
