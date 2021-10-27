var custom = {
    init: () => {
        this.eventResigter;
    },
    eventResigter: () => {
        window.click(() => {
            console.log("hello");
        })
    }
}
custom.init;
