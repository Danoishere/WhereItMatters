$(function () {
    'use strict'
    blueimp.Gallery(
        document.getElementById('links').getElementsByTagName('img'),
        {
            container: '#blueimp-gallery-carousel',
            carousel: true
        }
    );
});