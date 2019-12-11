import * as React from 'react';
import GridList from '@material-ui/core/GridList';
import GridListTile from '@material-ui/core/GridListTile';
import GridListTileBar from '@material-ui/core/GridListTileBar';
import ListSubheader from '@material-ui/core/ListSubheader';
import IconButton from '@material-ui/core/IconButton';
import InfoIcon from '@material-ui/icons/Info';
import { CardProps } from './Card';

export class TitlebarGridList extends React.Component<{ children: CardProps[] }> {
    render() {
        return (
            <div className='root container'>
                <GridList>
                    {this.props.children.map((card: CardProps, index: number) => (
                        <GridListTile key={index.toString()}>
                            <img src={card.previewImage} />
                            <GridListTileBar title={card.name} />
                        </GridListTile>
                    ))}
                </GridList>
            </div>
        )
    }
}