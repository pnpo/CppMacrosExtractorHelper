import React, { Component } from 'react';
import MyDropZone from '../../dropZone';

import Dropdown from './dropdown';


export default class Manually extends Component {

    render() {

        const mainWrapperStyle = {
            marginLeft: '20px',
            paddingRight: '30px'
        };

        return(
            <div style={mainWrapperStyle}>
               <h2>Manually</h2>
               <div style={{border:'2px dotted #000', marginBottom:'20px'}} >
                    <MyDropZone text ="Drop your Json File Here"/>
               </div>
               <div style={{border:'1px dotted #333', marginBottom:'10px'}}>
                   <Dropdown title={'Standards'}/>
               </div>
               <div style={{border:'1px dotted #333', marginBottom:'10px'}}>
                   <Dropdown title={'Compilers'}/>
               </div>
               <div style={{border:'1px dotted #333', marginBottom:'10px'}}>
                   <Dropdown title={'Libraries'}/>
               </div>
               <div style={{border:'1px dotted #333', marginBottom:'10px'}}>
                   <Dropdown title={'Operating_systems'}/>
               </div>
               <div style={{border:'1px dotted #333', marginBottom:'10px'}}>
                   <Dropdown title={'Architectures'}/>
               </div>
               <div style={{border:'1px dotted #333', marginBottom:'10px'}}>
                   <Dropdown title={'Custom'}/>
               </div>
            </div>
        );
    }
}