// This sets the image for the race you pick
function setImage(select) {
    var image = document.getElementsByName("image-swap")[0];
    image.src = select.options[select.selectedIndex].title;
}