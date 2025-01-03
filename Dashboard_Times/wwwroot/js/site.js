document.addEventListener("DOMContentLoaded", function () {
    // Obter o caminho atual da URL
    const currentPath = window.location.pathname;

    let activeLinkId;

    switch (currentPath) {
        case "/Home":
            activeLinkId = "overview";
            break;
        case "/Time":
        case "/Time/CadastrarTime":
        case "/Time/EditarTime":
            activeLinkId = "teams";
            break;
        case "/players":
            activeLinkId = "players";
            break;
        case "/championships":
            activeLinkId = "championships";
            break;
        default:
            activeLinkId = "overview";
            break;
    }

    if (activeLinkId) {
        document.getElementById(activeLinkId).classList.add("active");
    }
});
