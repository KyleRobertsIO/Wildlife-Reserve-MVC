/*
    Author: Kyle Roberts
*/

function mobileDropdownToggle() {
    let menu = document.getElementById('mobile-dropdown-menu');
    if (menu.style.display === "block") {
        menu.style.display = "none";
    } else {
        menu.style.display = "block";
    }
}

function closeMobileDropdown() {
    let menu = document.getElementById('mobile-dropdown-menu');
    menu.style.display = 'none';
}

window.addEventListener("resize", () => {
    if (window.innerWidth > 450) {
        closeMobileDropdown();
    }
});