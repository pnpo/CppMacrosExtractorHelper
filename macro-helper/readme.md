# 🚀 cx-web-kickstart 🚀🏈

This project serves as basic Checkmarx `React` web application startup.

## Usage

The original repository should be duplicated (mirrored)
```bash
$ git clone --bare http://tfs2015app:8080/tfs/DefaultCollection/External%20Enterprise/_git/cx-web-kickstart
```
Mirror-push to the new repository
```bash
$ cd cx-web-kickstart.git
$ git push --mirror http://tfs2015app:8080/tfs/DefaultCollection/[project-name]/[new-repository].git
```
Remove the temporary local repository you created in step 1.
```bash
$ cd ..
$ rm -rf cx-web-kickstart.git
```
> **Note:** The original cx-web-kickstart cannot be edit

After the repository duplicated, install app dependencies:
```bash
npm install
```
## Building and Running the application
For running the application in the browser in **development** mode run:
```bash
npm start
```
> **Note:** This will be the primary development mode

For building the application for **development** run:
> **Note:** Will output the artifact into `build` folder

```bash
npm run dev
```

For building the application for *production* run:
> **Note:** Will output the artifact into `dist` folder
```bash
npm run build
```

For run the unit-tests of the application run:
```bash
npm run test
```

For run the lint of the application run:
```bash
npm run lint
```

you're good to go!

## What's inside?

The primary dependencies of **cx-web-kickstart**:

|Package|Description
|----------------|-------------------------------
|[webpack](https://webpack.js.org/)|the app bundler
|[react](https://reactjs.org/)|the app views rendering engine
|[react-router-dom](https://reacttraining.com/react-router/web/guides/philosophy)|the app router
|[eslint](https://eslint.org/)|design time js lint
|[mobx](https://mobx.js.org)|state managment
|[jest](https://facebook.github.io/jest/)|app test runner
|[cypress](http://cypress.io)|end-to-end testing tool
|[oidc-client-js](https://github.com/IdentityModel/oidc-client-js)|OpenID Connect (OIDC) support for client-side

## Development guidlines

### Folders structure

#### Before you start, mind the following
* Long names should be style with [ellipsis](https://www.w3schools.com/cssref/css3_pr_text-overflow.asp)
* Consider add scroll bars for vertical and horizental growing boxes
    * Recomemnded: [react-custom-scrollbars](https://github.com/malte-wessel/react-custom-scrollbars)
* Consider paging for grids and long lists
* Consider Success/Failure handling for operations
* Disable form fields and buttons on submit
* Consider loader for async operations

## Folder Structure
The recommended folder structure is React modules. Means that feature have own folder with its core dependencies:

    ├── components    
    |   ├── button                  # folder name
    │   |   ├── index.jsx           # component entry point, normaly will be container
    │   |   ├── button.test.js      # test file
    │   |   ├── buttonLoader.jsx    # additional dependency non shared components
    │   |   ├── style.scss          # component styles
    │   └── ...                     
    └── ...

## Naming Convensions

#### Files

>  **Note:** All files should start with lower case

|type|filename|suffix|example|
|----|---|---|---|
|html|camelCase|*.html|index.html
|component|camelCase|*.jsx|cxComponent.jsx
|vanila JS|camelCase|*.js|userStoreValidator.js
|json|camelCase|*.json|package.json
|sass|camelCase|*.scss|controls.scss
|image|delimeter-seperated|*.png, *.svg|logo-icon.png, logo-icon.svg

#### css
class name should be delimeter-seperated. Example: `.blue-button`

#### component/container/class
React component must start with capital letter. Example: `Button`

# ESLint
ESLint and its plugins can help us find problems faster and enforce a consistent style guide across our code base

# React

## Clean Code
Our goal is to write clean and maintainable JSX code

## Component
React component should contain only the UI markup to render. Data and logic should pass by a *container* via `props`.

## Container
React container is a component which holds the data and logic for its composed components in the render method.

## Using the State
Use state when:
* Put into the state only the most minimal amount of data needed
* Put values to update when an event happens
* Put values which make the component re-render
* The question, if the data we are persisting is needed outside the component itself or by its children

Dont' use state when:
* Every time we can compute the final value from the props

Bad:
```js
class Price extends React.Component {

    constructor(props) { 
        
        super(props) 
 
        this.state = { 
            price: `${props.currency}${props.value}`
        }
    } 
 
    render() { 
        return <div>{this.state.price}</div> 
    }
}
```
Good:
```js
class Price extends React.Component {

    getPrice() {

        return `${this.props.currency}${this.props.value}`
    }

    render() {

        return <div>{this.getPrice()}</div>
    }
}
```

## Store
Imported store name should be camelCase in format

## Mobx: Optimizing rendering React components
### Use many small components

`@observer` components will track all values they use and re-render if any of them changes. So the smaller your components are, the smaller the change they have to re-render; it means that more parts of your user interface have the possibility to render independently of each other.

## Render lists in dedicated components
This is especially true when rendering big collections. React is notoriously bad at rendering large collections as the reconciler has to evaluate the components produced by a collection on each collection change. It is therefore recommended to have components that just map over a collection and render it, and render nothing else:

Bad:

```js
@observer class MyComponent extends Component {
    render() {
        const {todos, user} = this.props;
        return (<div>
            {user.name}
            <ul>
                {todos.map(todo => <TodoView todo={todo} key={todo.id} />)}
            </ul>
        </div>)
    }
}
```

In the above listing React will unnecessarily need to reconcile all TodoView components when the user.name changes. They won't re-render, but the reconcile process is expensive in itself.

Good:

```js
@observer class MyComponent extends Component {
    render() {
        const {todos, user} = this.props;
        return (<div>
            {user.name}
            <TodosView todos={todos} />
        </div>)
    }
}

@observer class TodosView extends Component {
    render() {
        const {todos} = this.props;
        return <ul>
            {todos.map(todo => <TodoView todo={todo} key={todo.id} />)}
        </ul>)
    }
}
```

### Don't use array indexes as keys
Don't use array indexes or any value that might change in the future as key. Generate id's for your objects if needed. See also this blog.

