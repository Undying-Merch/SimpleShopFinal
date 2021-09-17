import { event } from 'jquery';
import React, { Component, useEffect } from 'react';
import { useState } from 'react';

export class Customer extends Component {

    static displayName = Customer.name;

    constructor(props) {
        super(props);
        this.state = {
            Customers: [],
            CustomerID: '',
            CustomerName: '',
            CustomerAddress: '',
            CustomerPhone: '',
            CustomerEmail: '',
            ZipCode: '',

        }

        this.handleChangeID = this.handleChangeID.bind(this);
        this.handleChangeName = this.handleChangeName.bind(this);
        this.handleChangeAddress = this.handleChangeAddress.bind(this);
        this.handeChangePhone = this.handeChangePhone.bind(this);
        this.handleCangeEmail = this.handleCangeEmail.bind(this);
        this.handleChangeZip = this.handleChangeZip.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.deleteRow = this.deleteRow.bind(this);
        this.reloadPage = this.reloadPage.bind(this);
        this.updateRow = this.updateRow.bind(this);
        

    }
    handleChangeID(event) {
        this.setState({ CustomerID: event.target.value });
    }
    handleChangeName(event) {
        this.setState({ CustomerName: event.target.value });
    }
    handleChangeAddress(event) {
        this.setState({ CustomerAddress: event.target.value });
    }
    handeChangePhone(event) {
        this.setState({ CustomerPhone: event.target.value });
    }
    handleCangeEmail(event) {
        this.setState({ CustomerEmail: event.target.value });
    }
    handleChangeZip(event) {
        this.setState({ ZipCode: event.target.value });
    }
    handleSubmit(event){
        alert('A number has been changed: ' + this.state.CustomerID);
        event.preventDefault();
    }

    componentDidMount() {
        fetch('http://localhost:11700/Customer', {
            'method': 'GET'
        })
            .then(response => response.json())
            .then(response => {
                this.setState({
                    Customers: response
                });
            });
    }

    createRow(event) {
        

        fetch('http://localhost:11700/Customer', {
            'method': 'POST',
            'headers': {
                'content-type': 'application/json',
                'accept': 'application/json'
            },
            'body': JSON.stringify({
                "customerName": this.state.CustomerName,
                "customerAddress": this.state.CustomerAddress,
                "customerPhoneNumber": this.state.CustomerPhone,
                "customerEmail": this.state.CustomerEmail,
                "zipCode": this.state.ZipCode

            })
        })
                .then(response => response.json())
                .then(response => {
                    console.log(response);
                    this.reloadPage();
                })
            



    }

    deleteRow(event) {
        fetch('http://localhost:11700/Customer/' + this.state.CustomerID, {
            'method': 'DELETE'
        })
            .then(response => response.text())
            .then(response => {
                console.log(response);
                this.reloadPage();
            });
    }

    updateRow(event) {

        fetch('http://localhost:11700/Customer/' + this.state.CustomerID, {
            'method': 'PUT',
            'headers': {
                'content-type': 'application/json',
                'accept': 'application/json'
            },
            'body': JSON.stringify({
                "customerName": this.state.CustomerName,
                "customerAddress": this.state.CustomerAddress,
                "customerPhoneNumber": this.state.CustomerPhone,
                "customerEmail": this.state.CustomerEmail,
                "zipCode": this.state.ZipCode

            })
        })
            .then(response => response.json())
            .then(response => {
                console.log(response);
                this.reloadPage();
            })
    }

    reloadPage() {
        fetch('http://localhost:11700/Customer', {
            'method': 'GET'
        })
            .then(response => response.json())
            .then(response => {
                this.setState({
                    Customers: response
                });
            });
    }


    render() {

        return (
            < div >
                {
                    this.state.Customers && this.state.Customers.map(customer => 
                        <div key={customer.customerId}>
                            <p>ID: {customer.customerId} - Name: {customer.customerName} - Address: {customer.customerAddress} - Phone: {customer.customerPhoneNumber} - Mail: {customer.customerEmail} - City: {customer.zipCode} {customer.city}</p>
                        </div>
                    )
                }
                <p />
                <form onSubmit={this.handleSubmit}>
                    <label>
                        Customer ID
                    <input type="number" value={this.state.CustomerID} onChange={(e) => this.handleChangeID(e)} />
                    </label>
                </form>

               

                <form>
                    <label>
                        Delete Customer via ID
                    </label>
                    <button type="button" onClick={(e) => this.deleteRow(e)}>
                        Delete
                    </button>
                </form>

                <form>
                    <label>
                        Navn
                    </label>
                    <input type='text' value={this.state.CustomerName} onChange={(e) => this.handleChangeName(e)} />
                    <p />
                    <label>
                        Address
                    </label>
                    <input type='text' value={this.state.CustomerAddress} onChange={(e) => this.handleChangeAddress(e)} />
                    <p />
                    <label>
                        Phone
                    </label>
                    <input type='number' value={this.state.CustomerPhone} onChange={(e) => this.handeChangePhone(e)} />
                    <p />
                    <label>
                        Email
                    </label>
                    <input type='text' value={this.state.CustomerEmail} onChange={(e) => this.handleCangeEmail(e)} />
                    <p />
                    <label>
                        Postkode
                    </label>
                    <input type='number' value={this.state.ZipCode} onChange={(e) => this.handleChangeZip(e)} />
                </form>
                <p />
                <button type='button' onClick={(e) => this.createRow(e)}>
                    Create
                    </button>

                <p />
                <button type='button' onClick={(e) => this.updateRow(e)}>
                    Update
                </button>

                     


            </div>
            
                
        );
    }

}


  /*constructor(props) {
    super(props);
    this.state = { forecasts: [], loading: true };
  }

  componentDidMount() {
    this.populateWeatherData();
  }

  static renderForecastsTable(forecasts) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Date</th>
            <th>Temp. (C)</th>
            <th>Temp. (F)</th>
            <th>Summary</th>
          </tr>
        </thead>
        <tbody>
          {forecasts.map(forecast =>
            <tr key={forecast.date}>
              <td>{forecast.date}</td>
              <td>{forecast.temperatureC}</td>
              <td>{forecast.temperatureF}</td>
              <td>{forecast.summary}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderForecastsTable(this.state.forecasts);

    return (
      <div>
        <h1 id="tabelLabel" >Weather forecast</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async populateWeatherData() {
    const response = await fetch('weatherforecast');
    const data = await response.json();
    this.setState({ forecasts: data, loading: false });
  }*/
