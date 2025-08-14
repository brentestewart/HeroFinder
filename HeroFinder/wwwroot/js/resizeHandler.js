// wwwroot/js/resizeHandler.js
let handler = null;

export function registerResize(dotNetObjRef) {
    // avoid duplicate listeners
    if (handler) return;

    handler = () => {
        dotNetObjRef.invokeMethodAsync('OnResize', window.innerWidth, window.innerHeight);
    };
    window.addEventListener('resize', handler);

    // fire once so the component gets the initial size
    handler();
}

export function unregisterResize() {
    if (!handler) return;
    window.removeEventListener('resize', handler);
    handler = null;
}