import React, { Component } from 'react';
import Img from 'react-image';
import '../../styles/main.scss';

export default class Card extends Component {

    render() {
        const wrapper = {
           width:'100%',
           margin: '10px 0px 10px 0px'
        };
        
        const {image, text} = this.props;
        
        return(
            <div style={wrapper}>
                <button onClick={this.props.action} style={{width:'100%'}}>
                <table>
                <tbody>
                    <tr>
                        <td><Img style={{width:'50px'}} src={image}/></td>
                        <td>{text}</td>
                    </tr>
                </tbody>
                </table>
                </button>
            </div>
        );
    }
}