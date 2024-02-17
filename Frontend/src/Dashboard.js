import React, { Component } from 'react';
import * as signalR from '@microsoft/signalr';

export default class Dashboard extends Component {
  constructor() {
    super();

    this.state = {
      message: '',
      number: 1,
      stateTwo: {
        message: '',
        number: 1,
      },
    };
  }

  componentDidMount() {
    let connection = new signalR.HubConnectionBuilder()
      .withUrl("https://localhost:7250/chathub")
      .build();

    connection.start();

    connection.on("chatSection1", (data) => {
      console.log('DATA: ', data);
      this.setState({ number: this.state.number + 1 });
      this.setState({ message: data });
    });

    connection.on("chatSection2", (data) => {
      console.log(data);
      this.setState({ stateTwo: { ...this.state.stateTwo, message: data } });
    });
  }

  render() {
    return (
      <div>
        <hr></hr>
        <h1>Dashboard chatSection1</h1>
        {this.state.message}

        <hr></hr>
        <h1>Dashboard chatSection2</h1>
        {this.state.stateTwo.message}
      </div>
    );
  }
}
