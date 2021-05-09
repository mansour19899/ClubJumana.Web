function mymessage(xx) {
    var x = document.getElementById("snackbar");
    x.innerHTML = xx;
    x.className = "show";
    setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
}

let inactivityTime = function () {
    let time;
    window.onload = resetTimer;
    document.onmousemove = resetTimer;
    document.onkeypress = resetTimer;
    function logout() {
        document.getElementById("btnsessionExpird").click();
    }
    function resetTimer() {
        clearTimeout(time);
        time = setTimeout(logout, 900000)
    }
};
window.onload = function () {
    inactivityTime();
}


