import React from 'react';
import styles from './style.scss';

export const Info = props => 
    <div
        className={styles.info}
    >
         <div
            className={styles.icon}
        >
        🦉
        </div>
        <div>
            {props.children}
        </div>
    </div>;