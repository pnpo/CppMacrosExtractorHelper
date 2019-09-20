import React from 'react';
import ReactDOM from 'react-dom';
import { AppContainer } from 'react-hot-loader';

const MainPortalWrapper = require('./components/mainWrapper').default;

import './styles/fonts.scss';
import 'normalize.css/normalize.css';
import '@checkmarx/cx-ui/dist/cx-ui.css';

window.onload = function () {
    
    const render = Component => {
        ReactDOM.render(
            <AppContainer>
                <Component />
            </AppContainer>,
            document.getElementById('root')
        );
    };
    
    render(MainPortalWrapper);
    
    // webpack Hot Module Replacement API
    if (module.hot) {
        module.hot.accept('./components/mainWrapper', () => {
            const MainPortalWrapper = require('./components/mainWrapper').default;
            render(MainPortalWrapper);
    });
}
};