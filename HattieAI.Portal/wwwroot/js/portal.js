window.portalUi = {
    scrollToId: function (id) {
        const target = document.getElementById(id);
        if (!target) {
            return;
        }

        target.scrollIntoView({ behavior: "smooth", block: "start" });
    }
};
