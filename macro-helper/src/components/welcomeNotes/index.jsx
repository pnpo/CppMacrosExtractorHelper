import React from 'react';
import { TransparentButton, Button, ButtonType } from '@checkmarx/cx-ui/dist';
import { Info } from './info';
import styles from './style.scss';

export const WelcomeNotes = _ =>
    <div
        className={styles.root}
    >
        <div
            className={styles.mainTitle}
        >
            <span>
                Welcome to <b>cx-web-kickstart</b> Project!
            </span>
        </div>
        <h3>
            Who I'm?
        </h3>
            <p>
                I'm a <a href='https://en.wikipedia.org/wiki/Single-page_application' target='_blank'>SPA</a> that made with ❤️ with <code>react</code>, 
                and ready for you to use. I have the look and feel of <a href='https://www.checkmarx.com/' target='_blank'>Checkmarx</a>'s modern web applications 😎.
                <br />
                Want to see usage of me? Just take a look at <a
                href='http://tfs2013:8080/tfs/DefaultCollection/Enterprise%20Services/_git/Cx.Genesis?path=%2Fsrc%2FCx.Genesis.Presentation&version=GBmaster&_a=contents' target='_blank'>
                Access Control</a>, <a href='https://github.com/cxadmin/CxARM-Client' target='_blank'>CxARM</a> or even in <a href='http://tfs2013:8080/tfs/DefaultCollection/Enterprise%20Services/_git/Cx.DemoAccessControlClient' target='_blank'>this</a> small nice application.<br/>
                I'm supporting the major Browsers such <code>Chrome</code>, 
                <code>Firefox</code>, <code>Safari</code>, <code>Edge</code>, and even <code>IE11</code>!<br/>
            </p>
            <Info>
                For more technical details such how to run and deploy, 
                and for gull guidelines for how to develop in the application, please take a look in the <a href='http://tfs2013:8080/tfs/DefaultCollection/Enterprise%20Services/_git/cx-web-kickstart?path=%2Freadme.md&version=GBmaster&_a=contents' target='_blank'>README</a> file
            </Info>
        <h3>
            cx-ui
        </h3>
        <p>
            Most of the components are comming from my brother project <a href='http://cx-ui:3000' target='_blank'>cx-ui</a>. It's an <code>
                <a href='https://www.npmjs.com/package/@checkmarx/cx-ui' target='_blank'>npm</a></code> package, 
                but also a <code>Storybook</code> project.<br />
            Here some live example:
        </p>
        <p>
            <Button text='general button!' />
            <TransparentButton text='a transparent one!' />
            <Button buttonType={ButtonType.red} text='red one!' />
            <Button buttonType={ButtonType.green} text='and even a green one!' />
        </p>
        <h3>
            How to test me?
        </h3>
        <p>
            For <i>unit-tests</i> and <i>integration tests</i>, just use <code><a href='https://jestjs.io/' target='_blank'>Jest</a></code>. 
            Just run <code>npm test -- --watch</code> or <code>yarn test --watch</code> in the nearest command line and you're good to go!<br />
        </p>
        <p>
            For <i>E2E tests</i> use <code><a href='https://www.cypress.io' target='_blank'>Cypress</a></code>. 
            It's a super cool tool, and very easy and fun to use. Just run <code>npm run cy-dev</code> and watch the magic happens
        </p>
        <h3>
            State Management?
        </h3>
        <p>
            For the sake of simplicity I'm shipping with <code><a href='https://mobx.js.org/' target='_blank'>Mobx</a></code>. 
            But if you're in ❤️ with <code><a href='https://redux.js.org/' target='_blank'>Redux</a></code>, you can install it and use it!
        </p>
    </div>;