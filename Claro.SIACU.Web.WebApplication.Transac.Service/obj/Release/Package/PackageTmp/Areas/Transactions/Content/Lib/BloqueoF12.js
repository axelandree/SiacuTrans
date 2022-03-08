$(document).keydown(function (event) {
    if (event.keyCode == 123) { // Bloquear Tecla F12
        return false;
    } else if (event.ctrlKey && event.shiftKey && event.keyCode == 73) { // Crtl+Shift+I
        return false;
    } else if (event.ctrlKey && event.shiftKey && event.keyCode == 74) { // Ctrl+Shift+J
        return false;
    } else if (event.ctrlKey && event.keyCode == 85) { // Ctrl+U
        return false;
    } else if (event.ctrlKey && event.keyCode == 78) { // Ctrl+N
        return false;
    } else if (event.altKey && event.keyCode == 88) {
        return false;
    }
});

$(document).on("contextmenu", function (e) {
    e.preventDefault();
});
 
document.onkeydown = function (e) {
    tecla = (document.all) ? e.keyCode : e.which;
    if(tecla == 110) return false
}