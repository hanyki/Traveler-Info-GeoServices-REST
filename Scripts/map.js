/*global esri,dojo*/
/*jshint browser:true, debug:true */
require(["esri/map", "esri/layers/FeatureLayer"], function () {
    "use strict";
    var map, layer;

    map = new esri.Map("map", {
        basemap: "topo",
        center: [-120.48706054684378, 47.2717750663988],
        zoom: 7
    });

    layer = new esri.layers.FeatureLayer("TravelerInfo/FeatureServer/0");

    map.addLayer(layer);
});