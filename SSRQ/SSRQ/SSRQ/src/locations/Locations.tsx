import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { client } from '../shared';
import { Location } from '../dtos';

export default class Locations extends React.Component<any, any> {
    constructor(props) {
        super(props);
        this.state = {
            name: '',
            locations: []
        };
        this.nameChanged = this.nameChanged.bind(this);
    }

    componentDidMount() {
        this.getLocations();
    }

    async getLocations(name?: string) {
        let request = new Location();

        let r = await client.get(request);
        this.setState({
            locations: r.locations
        });
    }

    async addLocation() {
        let request = new Location();
        request.name = this.state.name;
        let r = await client.post(request);
        this.getLocations();
    }

    nameChanged(event) {
        this.setState({ name: event.target.value });
    }

    render() {
        let component = this;

        return (
            <div>
                <h4>Locations:</h4>
                <div className="form-inline form-group">
                    <input type="text" className="form-control" value={this.state.name} onChange={this.nameChanged} />
                    <button type="button" className="btn btn-primary"
                        onClick={() => { component.addLocation(); }}>Save location</button>
                </div>
                <br />
                <div>
                    {
                        <div className="well">
                            {
                                this.state.locations && this.state.locations.length && this.state.locations.map(function (item) {
                                    return (
                                        <div key={item.name}>
                                            <h6>
                                                {item.name}
                                            </h6>
                                        </div>
                                    )
                                })
                            }
                        </div>
                    }
                </div>
            </div>
        );
    }
}
