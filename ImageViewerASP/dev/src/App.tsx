import * as React from 'react';
import { render } from 'react-dom';

import * as Chapter from './Chapter';

// fake data
// create a directory `001` in `ImageViewerASP/dist` and push some images in it.
let data = {
    ImagePaths: [
        "001/001.jpg",
        "001/002.jpg",
        "001/003.jpg",
        "001/004.jpg",
        "001/005.jpg",
        "001/006.jpg",
        "001/007.jpg",
        "001/008.jpg",
        "001/009.jpg",
        "001/010.jpg",
        "001/011.jpg",
        "001/012.jpg",
        "001/014.jpg",
        "001/015.jpg",
        "001/016.jpg",
        "001/017.jpg",
        "001/018.jpg",
        "001/019.jpg",
        "001/020.jpg",
        "001/021.jpg",
        "001/022.jpg",
        "001/023.jpg",
        "001/024.jpg",
        "001/025.jpg",
        "001/026.jpg",
        "001/027.jpg",
        "001/028.jpg",
    ],
}

render(<Chapter.ChapterView chapter={data} />, document.getElementById('main'));