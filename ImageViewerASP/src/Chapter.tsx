import * as React from 'react';

export type ChapterProps = {
    ImagePaths: string[];
}

export class ChapterImage extends React.Component<{ imagePath: string }> {
    render() {
        return (
            <img src={this.props.imagePath} className="chapter-image" />
        );
    }
}

export class ChapterView extends React.Component<{ chapter: ChapterProps }>{
    render() {
        return (
            <div className="container flex">
                {this.props.chapter.ImagePaths.map((item, index) => (
                    <ChapterImage imagePath={item} key={index} />
                ))}
            </div>
        );
    }
}