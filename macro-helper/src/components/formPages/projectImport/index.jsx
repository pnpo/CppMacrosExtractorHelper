import React, { Component } from 'react';
import TextField from '@material-ui/core/TextField';
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import MyDropZone from '../../dropZone';
import { Button } from '@checkmarx/cx-ui';
import Divider from '@material-ui/core/Divider';

export default class ProjectImport extends Component {

    render() {

        const mainWrapperStyle = {
            marginLeft: '90px',
            paddingRight: '30px'
        };
        const rowsStyle={
            marginTop: '2%'
        };
        return(
            <div style={mainWrapperStyle}>
             <div style={{border:'2px dotted #000', marginBottom:'20px'}} >
           <Card>
            <CardContent>
                <MyDropZone text="Drop Project"></MyDropZone>     
             </CardContent>
           </Card>
           </div>
           <div style={rowsStyle}>
           <TextField
                id="textarea"
                multiline
                variant="filled"
                fullWidth = "auto"
             />
           </div>
           <div style={rowsStyle}>
           <Button text="Extract"></Button>

           </div>
          
            </div>
        );
    }
}