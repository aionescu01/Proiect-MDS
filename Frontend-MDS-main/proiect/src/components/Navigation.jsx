import React from "react";
import {NavLink} from "react-router-dom";
import './Navigation.css';
import AuthService from '../services/auth-service';
import EventBus from "../common/EventBus";
import { Component } from 'react';
import {Route, Link, Routes } from "react-router-dom";
import Login from "./Login_component";
import Register from "./Register_component";
import Profile from "./Profile_component";

class Navigation extends Component {
  constructor(props) {
    super(props);
    this.logOut = this.logOut.bind(this);

    this.state = {
      currentUser: undefined,
    };
  }

  componentDidMount() {
    const user = AuthService.getCurrentUser();

    if (user) {
      this.setState({
        currentUser: user,
      });
    }
    
    EventBus.on("logout", () => {
      this.logOut();

    }); 
  }

  componentWillUnmount() {
    EventBus.remove("logout");
  }

  logOut() {
    AuthService.logout();
    this.setState({
      currentUser: undefined,
    });
  }

  render(){
    const { currentUser } = this.state;
  return (
    <div className="navigation ">
      <nav className="navbar navbar-expand  ">
        <div className="container">

          <div>
            <ul className="navbar-nav ml-auto">
              <li className="nav-item">
                <NavLink className="nav-link" to="/">
                  Home
                  <span className="sr-only">(current)</span>
                </NavLink>
              </li>
              <li className="nav-item">
                <NavLink className="nav-link" to="/Players">
                  Players
                </NavLink>
              </li>
              <li className="nav-item">
                <NavLink className="nav-link" to="/Staff">
                  Staff
                </NavLink>
              </li>
              <li className="nav-item">
                <NavLink className="nav-link" to="/Player_Contracts">
                  Players Contract
                </NavLink>
              </li>
              <li className="nav-item">
                <NavLink className="nav-link" to="/Staff_Contracts">
                  Staff Contract
                </NavLink>
              </li>
              <li className="nav-item">
                <NavLink className="nav-link" to="/Matches">
                  Matches
                </NavLink>
              </li>
              <li className="nav-item">
                <NavLink className="nav-link" to="/Stadiums">
                  Stadiums
                </NavLink>
              </li>
              <li className="nav-item">
                <NavLink className="nav-link" to="/stats">
                  Stats
                </NavLink>
              </li>
              {currentUser ? (
            <div className="navbar-nav ml-auto">
              <li className="nav-item">
                <Link to={"/profile"} className="nav-link">
                  {currentUser.firstName} {currentUser.lastName}
                </Link>
              </li>
              <li className="nav-item">
                <a href="/login" className="nav-link" onClick={this.logOut}>
                  Log Out
                </a>
              </li>
            </div>
          ) : (
            <div className="navbar-nav ml-auto">
              <li className="nav-item">
                <Link to={"/login"} className="nav-link">
                  Login
                </Link>
              </li>

              <li className="nav-item">
                <Link to={"/register"} className="nav-link">
                  Sign Up
                </Link>
              </li>
            </div>
          )}
            </ul>
          </div>
        </div>
      </nav>
    </div>
  );
}}
export default Navigation;
