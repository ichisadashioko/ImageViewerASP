import * as React from 'react'
import { render } from 'react-dom'
import { TitlebarGridList } from './TitlebarGridList'
import { CardProps } from './Card'

class Index extends React.Component<{}, { children: CardProps[] }> {
    componentDidMount() {
        let url = window.location.href;
        console.log(`url: ${url}`);

        let xhr = new XMLHttpRequest();
        xhr.open('GET', url, true);
        xhr.setRequestHeader('Content-Type', 'application/json')

        xhr.onload = () => {
            let data = JSON.parse(xhr.responseText)

            console.log(data)
            this.setState({
                children: data,
            })
        }

        xhr.send()
    }
    render() {
        return <TitlebarGridList children={this.state.children} />
    }
}

render(<Index />, document.getElementById('app'))