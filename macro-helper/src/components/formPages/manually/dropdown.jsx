import React, { Component } from 'react';

import TextField from '@material-ui/core/TextField';

export default class Dropdown extends Component {

    render() {

        const mainWrapperStyle = {
            padding: '10px'
        };
        const {title} = this.props;
        return(
            <div style={mainWrapperStyle}>
               <h3>{title} </h3>
               <form>
               <TextField />
               </form>
            </div>
        );
    }
}