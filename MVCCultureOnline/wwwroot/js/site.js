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

document.addEventListener('DOMContentLoaded', function () {
    const passwordInput = document.querySelector('input[name="Contrasena"]');

    if (!passwordInput) return; // Por si no está en la página

    passwordInput.addEventListener('input', () => {
        const val = passwordInput.value;

        // Longitud
        document.getElementById('lengthReq').className = (val.length >= 8 && val.length <= 20) ? 'mb-1 text-success' : 'mb-1 text-danger';

        // Letra mayúscula
        document.getElementById('upperReq').className = /[A-Z]/.test(val) ? 'mb-1 text-success' : 'mb-1 text-danger';

        // Letra minúscula
        document.getElementById('lowerReq').className = /[a-z]/.test(val) ? 'mb-1 text-success' : 'mb-1 text-danger';

        // Número
        document.getElementById('numberReq').className = /\d/.test(val) ? 'mb-1 text-success' : 'mb-1 text-danger';

        // Carácter especial
        document.getElementById('specialReq').className = /[!@#$%^&*]/.test(val) ? 'mb-1 text-success' : 'mb-1 text-danger';
    });
});


