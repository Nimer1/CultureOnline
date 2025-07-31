// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", function () {
    const toggleBtn = document.getElementById("toggleReseñas");
    const extraReseñas = document.querySelectorAll(".reseña-extra");

    if (toggleBtn) {
        let showing = false;

        toggleBtn.addEventListener("click", function () {
            showing = !showing;

            extraReseñas.forEach(el => {
                el.classList.toggle("d-none", !showing);
            });

            toggleBtn.innerHTML = showing
                ? '<i class="fas fa-chevron-up"></i> Ocultar reseñas'
                : '<i class="fas fa-chevron-down"></i> Mostrar más reseñas';
        });
    }
});


document.addEventListener("DOMContentLoaded", function () {
    const itemsPorPagina = 2;
    let paginaActual = 1;
    const reseñas = document.querySelectorAll(".reseña-item");
    const totalPaginas = Math.ceil(reseñas.length / itemsPorPagina);

    const btnAnterior = document.getElementById("btnAnterior");
    const btnSiguiente = document.getElementById("btnSiguiente");
    const paginaActualSpan = document.getElementById("paginaActual");
    const totalPaginasSpan = document.getElementById("totalPaginas");

    totalPaginasSpan.textContent = totalPaginas;

    function mostrarPagina(pagina) {
        paginaActual = pagina;
        const inicio = (pagina - 1) * itemsPorPagina;
        const fin = inicio + itemsPorPagina;

        reseñas.forEach((reseña, i) => {
            reseña.style.display = (i >= inicio && i < fin) ? "block" : "none";
        });

        paginaActualSpan.textContent = paginaActual;
        btnAnterior.disabled = paginaActual === 1;
        btnSiguiente.disabled = paginaActual === totalPaginas;
    }

    btnAnterior.addEventListener("click", () => {
        if (paginaActual > 1) {
            mostrarPagina(paginaActual - 1);
        }
    });

    btnSiguiente.addEventListener("click", () => {
        if (paginaActual < totalPaginas) {
            mostrarPagina(paginaActual + 1);
        }
    });

    mostrarPagina(1);
});

//Validaciones para los campos del formulario de registro
function validatePassword(password) {
    // Se obtiene el elemento tooltip que muestra las reglas de validación
    const tooltip = document.getElementById("passwordTooltip");

    // Objeto que define las reglas de validación y verifica si se cumplen:
    const rules = {
        length: password.length >= 8 && password.length <= 20, // Longitud entre 8-20 caracteres
        upper: /[A-Z]/.test(password), // Contiene al menos una mayúscula
        lower: /[a-z]/.test(password), // Contiene al menos una minúscula
        number: /[0-9]/.test(password), // Contiene al menos un número
        special: /[!@@#$%^&*]/.test(password) // Contiene al menos un carácter especial
    };

    // Actualizar la interfaz para cada regla:
    // Cambia a texto verde si se cumple la regla
    // Cambia a texto rojo si no se cumple
    document.getElementById("ruleLength").className = rules.length ? "text-success" : "text-danger";
    document.getElementById("ruleUpper").className = rules.upper ? "text-success" : "text-danger";
    document.getElementById("ruleLower").className = rules.lower ? "text-success" : "text-danger";
    document.getElementById("ruleNumber").className = rules.number ? "text-success" : "text-danger";
    document.getElementById("ruleSpecial").className = rules.special ? "text-success" : "text-danger";

    // Aquí se verificar si todas las reglas se cumplen (isValid = true si todas son true)
    const isValid = Object.values(rules).every(val => val);
    // Muestra u oculta el tooltip según
    // Lo oculta si la contraseña es válida o está vacía
    // Lo muestra si hay contenido pero no cumple todas las reglas
    tooltip.classList.toggle("d-none", isValid || password.length === 0);
}


