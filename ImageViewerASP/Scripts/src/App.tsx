import * as React from 'react';
import { render } from 'react-dom';

import * as Chapter from './Chapter';

// fake data
// create a directory `001` in `../dist` and push some images in it.
let data = {
    ImagePaths: [
        "dist/001/001.jpg",
        "dist/001/002.jpg",
        "dist/001/003.jpg",
        "dist/001/004.jpg",
        "dist/001/005.jpg",
        "dist/001/029.jpg",
    ],
}

render(<Chapter.ChapterView chapter={data} />, document.getElementById('main'));