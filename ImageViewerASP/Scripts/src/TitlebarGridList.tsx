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
            <div className='root'>
                <GridList cellHeight={180} className='gridList'>
                    <GridListTile key="Subheader" cols={2} style={{ height: 'auto' }}>
                        <ListSubheader component="div">December</ListSubheader>
                    </GridListTile>
                    {this.props.children.map((card: CardProps, index: number) => (
                        <GridListTile key={index.toString()}>
                            <img src={card.previewImage} />
                            <GridListTileBar
                                title={card.name}
                                actionIcon={
                                    <IconButton aria-label={`info about ${card.name}`} className='icon'>
                                        <InfoIcon />
                                    </IconButton>
                                }
                            />
                        </GridListTile>
                    ))}
                </GridList>
            </div>
        )
    }
}