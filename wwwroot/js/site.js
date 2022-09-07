// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Example starter JavaScript for disabling form submissions if there are invalid fields
(function () {
    'use strict';
    window.addEventListener('load', function () {
        // Fetch all the forms we want to apply custom Bootstrap validation styles to
        var forms = document.getElementsByClassName('needs-validation');
        // Loop over them and prevent submission
        var validation = Array.prototype.filter.call(forms, function (form) {
            form.addEventListener('submit', function (event) {
                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                }
                form.classList.add('was-validated');
            }, false);
        });
    }, false);
})();

// password eye icon 
feather.replace({ 'aria-hidden': 'true' });

$(".togglePassword").click(function (e) {
    e.preventDefault();
    var type = $(this).parent().parent().find(".password").attr("type");
    console.log(type);
    if (type == "password") {
        $("svg.feather.feather-eye").replaceWith(feather.icons["eye-off"].toSvg());
        $(this).parent().parent().find(".password").attr("type", "text");
    } else if (type == "text") {
        $("svg.feather.feather-eye-off").replaceWith(feather.icons["eye"].toSvg());
        $(this).parent().parent().find(".password").attr("type", "password");
    }
});

//nav open and close effect 
function openNav() {
    document.getElementById("mySidebar").style.width = "190px";

    var elements = document.getElementsByClassName("animation");
    var content = document.getElementsByClassName("content-animation");

    for (var i = 0; i < elements.length; i++) {
        elements[i].style.marginLeft = "190px"
    }

    for (var i = 0; i < content.length; i++) {
        content[i].style.left = "56.5%";
    }
}

function closeNav() {
    document.getElementById("mySidebar").style.width = "0";

    var elements = document.getElementsByClassName("animation");
    var content = document.getElementsByClassName("content-animation");

    for (var i = 0; i < elements.length; i++) {
        elements[i].style.marginLeft = "0px";
    }

    for (var i = 0; i < content.length; i++) {
        content[i].style.left = "50%";
    }
}