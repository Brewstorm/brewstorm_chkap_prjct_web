// Setup Polymer options
window.Polymer = {
    dom: 'shadow',
    lazyRegister: true
};

// Load webcomponentsjs polyfill if browser does not support native Web Components
(function () {
    'use strict';

    var onload = function () {
        // For native Imports, manually fire WebComponentsReady so user code
        // can use the same code path for native and polyfill'd imports.
        if (!window.HTMLImports) {
            document.dispatchEvent(
              new CustomEvent('WebComponentsReady', { bubbles: true })
            );
        }
    };

    var webComponentsSupported = (
      'registerElement' in document
      && 'import' in document.createElement('link')
      && 'content' in document.createElement('template')
    );

    if (!webComponentsSupported) {
        var script = document.createElement('script');
        script.async = true;
        script.src = '/lib/webcomponentsjs/webcomponents-lite.min.js';
        script.onload = onload;
        document.head.appendChild(script);
    } else {
        onload();
    }
})();

// Load pre-caching Service Worker
if ('serviceWorker' in navigator) {
    window.addEventListener('load', function () {
        navigator.serviceWorker.register('/js/service-worker.js');
    });
}