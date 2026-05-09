// Paths to the two images
const image1 = "Lab1/Images/LabOne001";
const image2 = "Lab1/Images/LabOne002";

// Keeps track of which image is currently shown
let showingFirstImage = true;

// Get references to HTML elements
const displayedImage = document.getElementById("displayedImage");
const switchButton = document.getElementById("switchButton");

// Set the initial image when the page loads
displayedImage.src = image1;

// Switch between images when the button is clicked
switchButton.addEventListener("click", function () {

    // Check which image is currently displayed and switch
    if (showingFirstImage) {
        displayedImage.src = image2;
    } else {
        displayedImage.src = image1;
    }

    // Flip the boolean so next click switches again
    showingFirstImage = !showingFirstImage;
});
