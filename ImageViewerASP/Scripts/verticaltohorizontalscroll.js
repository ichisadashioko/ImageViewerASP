
let mangaContainer = document.getElementById('manga-container')

/**
 * @param {WheelEvent} evt 
 */
function mapVerticalToHorizontalScrolling(evt) {
    // console.log(evt)
    if (evt.deltaX === 0) {
        evt.preventDefault()
        mangaContainer.scrollLeft += evt.deltaY
    }
}

if (mangaContainer) {
    mangaContainer.addEventListener('wheel', mapVerticalToHorizontalScrolling)
}