import React, { Component } from 'react';
import PropTypes from 'prop-types';
import classNames from 'classnames';
import styles from './style.scss';

export default class Button extends Component {

    // constructor(props) {

    //         super(props);

    //         this.state = {
    //             color: 'red',
    //             toggle: false
    //         }
    // }

    // onClick = () => {

    //     this.setState({
    //         color: 'blue'
    //     });
    // }

    render() {

        const { text, disabled, onClick } = this.props;

        const cssClass = classNames(
            styles.rootButton,
            disabled && styles.disabled
        );

        return(
            <button 
                className={cssClass}
                onClick={onClick}
                
            >
                {text}
            </button>
        );
    }
}

Button.propTypes = {
    text: PropTypes.string
};