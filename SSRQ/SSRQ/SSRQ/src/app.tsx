import 'bootstrap/dist/css/bootstrap.css';
import 'font-awesome/css/font-awesome.css';
import './app.scss';

import "es6-shim";
import * as ReactDOM from 'react-dom';
import * as React from 'react';

import { BrowserRouter as Router, Route, Link } from 'react-router-dom';
import { NavItem } from './shared';

import Home from './home/Home';
import View1 from './view1/View';
import View2 from './view2/View';
import Clients from './clients/Clients';
import Locations from './locations/Locations';

class App extends React.Component<any, any> {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <Router>
            <div>
                <nav className="navbar navbar-expand-lg navbar-dark">
                    <div className="container">
                        <Link to="/" className="navbar-brand"> 
                            <i className="fa fa-code" aria-hidden="true"></i>
                            <span style={{ paddingLeft: 5 }}>SSRQ ZOsKI</span>
                        </Link>
                        <div className="navbar-collapse">
                            <ul className="navbar-nav mr-auto">
                                <NavItem to="/">Home</NavItem>
                                <NavItem to="/client">Clients (MongoDb)</NavItem>
                                <NavItem to="/location">Locations (Redis Cache)</NavItem>
                            </ul>
                        </div>
                    </div>
                </nav>

                <div className="container">
                    <div className="row" style={{ margin: "10px 0" }}>
                        <div id="content">
                            <Route exact path="/" render={props => <Home name="React" />} activeClassName="active" />
                            <Route path="/client" component={Clients} activeClassName="active" />
                            <Route path="/location" component={Locations} activeClassName="active" />
                            <Route path="/view1" component={View1} activeClassName="active" />
                            <Route path="/view2" component={View2} activeClassName="active" />
                        </div>
                    </div>
                </div>
            </div>
            </Router>
        );
    }
}

ReactDOM.render(<App />, document.getElementById("app"));
