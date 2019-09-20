import React, { Component } from 'react';

export class Counter extends Component {
  displayName = Counter.name

  constructor(props) {
    super(props);
    this.state = { currentCount: 0 };
    this.incrementCounter = this.incrementCounter.bind(this);
  }

    incrementCounter(event) {
        var libpath = document.getElementById("lib_path").value;
        var libname = document.getElementById("lib_name").value;
        fetch("api/Analyse/library/" + libpath + "/" + libname).then(response => var x = response);
            
  }

  render() {
    return (
        <div>
            <br />
            <input type="Text" id="lib_path" placeholder="Library Path" />
            <br />
            <input type="text" id="lib_name" placeholder="Library Name" />
            <button onClick={this.incrementCounter}  >Submit </button>
      </div>
    );
  }
}
