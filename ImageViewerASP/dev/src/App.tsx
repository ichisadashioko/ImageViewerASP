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
        "001/029.jpg",
    ],
}

render(<Chapter.ChapterView chapter={data} />, document.getElementById('main'));