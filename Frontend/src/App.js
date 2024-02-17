import React, { Component } from 'react';

import * as signalR from '@microsoft/signalr'

export default class App extends Component {

  constructor() {
    super();

    this.state = {
      message: [],
      number: 1,
    }
  }
  componentDidMount() {
    let connection = new signalR.HubConnectionBuilder()
      .withUrl("https://localhost:7250/chathub")
      .build();

      connection.start()

      connection.on("chatSection1", (data) => {
            console.log('DATA: ', data);
            this.setState({ number: this.state.number + 1 })
      })
      
  //   connection.on("chatSection2", data => {
  //     console.log(data);
  // });
  }
  handleButtonClick = () => {
    this.fetchData(this.state.number+1);
  }

  fetchData = (n) => {
    fetch(`https://localhost:7250/api/Chat/send/test1?count=${n}`);
  }

  render() {
    return (
      <div>
      <h1>React app with Dotnet core signal-r</h1> 
        <button onClick={this.handleButtonClick}>
          click
        </button> <br /><br />
        {this.state.number}
      </div>
    )
  }
}

