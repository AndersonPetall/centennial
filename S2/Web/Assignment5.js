"use strict";

var source = 0;
var canvas = document.getElementById("mycanvas");
var cxt = canvas.getContext("2d");
let x = 250,
  y = 300,
  wh = 40,
  dx = 2,
  dy = 2,
  speed = 1;
const img = new Image();
img.src = "./bug.gif";

window.onload = function () {
  function angule(a, b) {
    cxt.clearRect(0, 0, canvas.width, canvas.height);
    cxt.beginPath();
    cxt.drawImage(img, a, b, wh, wh);
    cxt.closePath();
  }
  setInterval(function () {
    dx = (Math.floor(Math.random() * 3) - 1) * 3;
    dy = (Math.floor(Math.random() * 3) - 1) * 3;
    if (x + dx * speed + wh > canvas.width || x + dx * speed < 0) {
      dx = -dx;
    }
    if (y + dy * speed + wh > canvas.height || y + dy * speed < 0) {
      dy = -dy;
    }
    x += dx * speed;
    y += dy * speed;
    angule(x, y);
  }, 20);
  canvas.onclick = function (e) {
    e = e || event;
    var px = e.offsetX;
    var py = e.offsetY;
    if (px >= x && px <= x + wh && py >= y && py <= y + wh) {
      source += speed;
      document.getElementById("source").innerHTML = source;
      document.getElementById("speed").innerHTML = speed;
      refersh();
    }

    function refersh() {
      x = Math.floor(Math.random() * canvas.width - wh);
      y = Math.floor(Math.random() * canvas.height - wh);
      speed += 1;
    }
    document.getElementById("resetSource").addEventListener(
      "click",
      function () {
        document.getElementById("source").innerHTML = 0;
        source = 0;
      },
      false
    );
    document.getElementById("resetSpeed").addEventListener(
      "click",
      function () {
        speed = 1;
        document.getElementById("speed").innerHTML = 1;
      },
      false
    );
  };
};
