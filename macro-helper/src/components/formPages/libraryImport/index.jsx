import React, { Component } from 'react';
import TextField from '@material-ui/core/TextField';
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';


import MyDropZone from '../../dropZone';
import { Button } from '@checkmarx/cx-ui';

export default class LibraryImport extends Component {

    render() {

        const mainWrapperStyle = {
            marginLeft: '90px',
            paddingRight: '30px'
        };

    return(
        <div style={mainWrapperStyle}>
           <div>
           <TextField
                required
                id="outlined-full-width"
                label="Name"
                style={{ margin: 8 }}
                placeholder="Placeholder"
                helperText="Name your Library!"
                fullWidth
                margin="normal"
                variant="outlined"
                InputLabelProps={{
                shrink: true
                
                }}
            />
           </div>
           <br/>
           <div style={{border:'2px dotted #000', marginBottom:'20px'}} >
           <Card>
            <CardContent>
                <MyDropZone required text="Drop your library here"></MyDropZone>     
             </CardContent>
           </Card>
           </div>
                <Button text="Extract"></Button>
        </div>
        );
    }
}