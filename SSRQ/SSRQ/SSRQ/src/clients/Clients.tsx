import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { client } from '../shared';
import { Client } from '../dtos';

export default class Clients extends React.Component<any, any> {
    constructor(props) {
        super(props);
        this.state = {
            response: null,
            clients: []
        };
        this.searchClients = this.searchClients.bind(this);
    }

    componentDidMount() {
        this.getClients();
    }

    searchClients(event) {
        let value = event.target.value;
        this.getClients(value);
    }

    async getClients(name?: string) {
        let request = new Client();
        if (name != null) {
            request.name = name;
        }

        let r = await client.get(request);
        this.setState({
            response: r.response,
            clients: r.clients
        });
    }

    async sendMessage(clientObj: any) {
        let request = new Client();
        request.id = clientObj.id;
        let r = await client.post(request);
        alert(r.response);
    }

    async addClient() {
        let request = new Client();
        let r = await client.put(request);
        this.getClients();
    }

    render() {
        let component = this;

        return (
            <div>
                <h4>Clients:</h4>
                <p>
                    <button type="button" className="btn btn-primary"
                        onClick={() => { component.addClient(); }}>ADD NEW</button>
                </p>
                <div>
                    {
                        <div className="well">
                            <div>
                                <input className="form-control" type="text" placeholder="Your name"
                                    onChange={this.searchClients} />
                            </div>
                            {
                                this.state.clients && this.state.clients.length && this.state.clients.map(function (client) {
                                    return (
                                        <div key={client.id}>
                                            <h6>
                                                Id: {client.id}
                                                <span style={{ marginLeft: 20, marginRight: 20 }}>{client.name}</span>
                                                <button type="button" className="btn btn-primary"
                                                    onClick={() => { component.sendMessage(client); }}>Notify</button>
                                            </h6>
                                        </div>
                                    )
                                })
                            }
                        </div>
                        //this.state.response == null
                        //    ?
                        //    (<p>No clients loaded.</p>)
                        //    :
                        //    ()
                            
                    }
                </div>
            </div>
        );
    }
}
