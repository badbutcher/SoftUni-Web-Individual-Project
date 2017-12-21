// This sets the image for the race you pick
function setImage(select) {
    var image = document.getElementsByName("image-swap")[0];
    image.src = select.options[select.selectedIndex].title;
}

// Sums the unit price
function MineralsSum(value) {
    var minerals = 0;
    minerals += new Number(value);
    document.getElementById('sumM').innerHTML = minerals;
}

function GasSum(value) {
    var gas = 0;
    gas += new Number(value);
    document.getElementById('sumG').innerHTML = gas;
}