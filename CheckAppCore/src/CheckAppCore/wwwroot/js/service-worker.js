var cacheName = 'checkAppCache';
var dataCacheName = 'checkAppCacheData-v1.1';

var filesToCache = [
    '/',
    '/styles/site.css',
    '/styles/checkapp-styles.html',
    '/js/site.js',
    '/images/ca_logo.png',
    '/images/ca_marca.png',
    '/images/bg_home.jpg'
];

self.addEventListener('install', function (e) {
    console.log('[ServiceWorker] Install');
    e.waitUntil(
      caches.open(cacheName).then(function (cache) {
          console.log('[ServiceWorker] Caching app shell');
          return cache.addAll(filesToCache);
      })
    );
});

self.addEventListener('activate', function (e) {
    console.log('[ServiceWorker] Activate');
    e.waitUntil(
      caches.keys().then(function (keyList) {
          return Promise.all(keyList.map(function (key) {
              console.log('[ServiceWorker] Removing old cache', key);
              if (key !== cacheName) {
                  return caches.delete(key);
              }
          }));
      })
    );
});

self.addEventListener('fetch', function (e) {
    console.log('[ServiceWorker] Fetch', e.request.url);
    var dataUrl = 'https://publicdata-weather.firebaseio.com/';
    if (e.request.url.indexOf(dataUrl) === 0) {
        e.respondWith(
          fetch(e.request)
            .then(function (response) {
                return caches.open(dataCacheName).then(function (cache) {
                    cache.put(e.request.url, response.clone());
                    console.log('[ServiceWorker] Fetched&Cached Data');
                    return response;
                });
            })
        );
    } else {
        e.respondWith(
          caches.match(e.request).then(function (response) {
              return response || fetch(e.request);
          })
        );
    }
});