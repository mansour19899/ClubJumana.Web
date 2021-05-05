function mymessage(xx) {
    var x = document.getElementById("snackbar");
    x.innerHTML = xx;
    x.className = "show";
    setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
}