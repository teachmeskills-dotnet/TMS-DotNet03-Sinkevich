var imgs = new Array("images/img1.jpg", "images/img2.jpg", "images/img3.jpg", "images/img4.jpg", "images/img5.jpg");
function changeBg() {
    var imgUrl = imgs[Math.floor(Math.random() * imgs.length)];
    $('#header').css('background-image', 'url(' + imgUrl + ')');
    $('#header').fadeIn(1000); //this is new, will fade in smoothly
}

function changeBackgroundSmoothly() {
    $('#header').fadeOut(1000, changeBg); //this is new, will fade out smoothly
}

setInterval(changeBackgroundSmoothly, 10000);