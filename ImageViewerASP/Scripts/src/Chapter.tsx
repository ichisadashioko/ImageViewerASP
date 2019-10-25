import * as React from 'react';

export interface LooseObject {
    [key: string]: any
}
export type ChapterProps = {
    ImagePaths: string[];
}

export class ChapterImage extends React.Component<{ imagePath: string, ref: string }> {
    render() {
        return (
            <img src={this.props.imagePath} className="chapter-image" ref={this.props.ref} />
        );
    }
}

type ChapterViewProps = {
    chapter: ChapterProps;
    itemRefs: any[];
}
type ChapterViewState = {
    currentIndex: number;
}


export class ChapterView extends React.Component<ChapterViewProps, ChapterViewState>{
    state = {
        currentIndex: 0,
    }
    constructor(props: ChapterViewProps) {
        super(props)
        // props.itemRefs = props.chapter.ImagePaths.map((v, idx) => React.createRef())
        // console.log(typeof props)
        // console.log(props)
    }
    scrollToRef = (ref: any) => {
        console.log(ref)
        window.scrollTo(0, ref.current.offsetTop)
    }
    scrollIntoView(id: any) {
        console.log(id)
        console.log(`typeof(id): ${typeof (id)}`)
        this.scrollToRef(id)
    }
    handleKeyDown(event: any) {
        let evt = event as KeyboardEvent;
        console.log(evt.key)
        if (evt.key === 'ArrowLeft') {
            this.scrollIntoView(this.state.currentIndex)
            this.setState({
                currentIndex: this.state.currentIndex++,
            })
        }
    }
    render() {
        return (
            <div className="container flex"
                onKeyDown={this.handleKeyDown}
                tabIndex={0}>
                {this.props.chapter.ImagePaths.map((item, index) => (
                    <ChapterImage imagePath={item} key={index} ref={index.toString()} />
                ))}
            </div>
        );
    }
}