var mangaContainer = document.getElementById('manga-container')

console.log(mangaContainer)

document.body.addEventListener('keyup', function (ev) {
    // console.log(ev)

    if (ev.key === 'ArrowDown') {
        mangaContainer.scrollBy({
            left: -(window.innerWidth / 3),
            behavior: 'auto',
        })
    } else if (ev.key === 'ArrowUp') {
        mangaContainer.scrollBy({
            left: (window.innerWidth / 3),
            behavior: 'auto',
        })
    } else if (ev.key === 'PageDown') {
        mangaContainer.scrollBy({
            left: -(window.innerWidth * 0.75),
            behavior: 'auto',
        })
    } else if (ev.key === 'PageUp') {
        mangaContainer.scrollBy({
            left: (window.innerWidth * 0.75),
            behavior: 'auto',
        })
    } else {
        console.log(ev)
    }
})
