document.addEventListener("DOMContentLoaded", function () {
    // Obter o caminho atual da URL
    const currentPath = window.location.pathname;

    let activeLinkId;

    if (currentPath.startsWith("/Home")) {
        activeLinkId = "overview";
    }
    else if (currentPath.startsWith("/Time")) {
        activeLinkId = "teams";

    }
    else if (currentPath.startsWith("/Jogador")) {
        activeLinkId = "players";

    }
    else if (currentPath.startsWith("/championships")) {
        activeLinkId = "championships";

    }
    else {
        activeLinkId = "overview";
    }

    if (activeLinkId) {
        document.getElementById(activeLinkId).classList.add("active");
    }
});
