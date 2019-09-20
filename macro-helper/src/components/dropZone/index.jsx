import React, { Component } from 'react';
import Dropzone from 'react-dropzone';

class MyDropZone extends Component {
  constructor(props) {
    super(props);
  }
    render() {
      const {text} = this.props;

      return (<div>
              <Dropzone onDrop={acceptedFiles =>(acceptedFiles)}>
                    {({getRootProps, getInputProps}) => (
                        <section>
                        <div {...getRootProps()}>
                            <input {...getInputProps()} />
                            <p align="center">{text}</p>
                        </div>
                        </section>
                    )}
              </Dropzone>
            </div>);
    }
  }
 
  export default MyDropZone;