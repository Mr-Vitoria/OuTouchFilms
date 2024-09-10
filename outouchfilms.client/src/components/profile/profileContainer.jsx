import React, { Component } from "react";

import { getCookie } from "../../functions/cookie";

import { UserInfo } from "./models/userInfo";
import { getUser } from "../../api/userService";
import { BookmarkList } from "./models/bookmarkList";
import { PageLoader } from "../shared/pageLoader";

export class ProfileContainer extends Component {

    constructor(props) {
        super(props);
        this.state = {
            user: null
        };

        this.pageLoaderRef = React.createRef();

        this.getUserInfoEvent = this.getUserInfoEvent.bind(this);
    }

    componentDidMount() {
        const token = getCookie('token');
        if (token == undefined) {
            window.location.assign('login');
        }
        this.getUserInfoEvent(token);
    }

    async getUserInfoEvent(token) {
        const user = await getUser(token);

        if (user != undefined) {
            this.setState({
                user: user
            });
            this.pageLoaderRef.current.close();
        }
    }

    render() {
        return <>
            {
                this.state.user != null ?
                    <>
                        <UserInfo user={this.state.user} />
                        <BookmarkList userId={this.state.user.id} />
                    </>
                    : null
            }

            <PageLoader ref={this.pageLoaderRef} />
        </>

    }
}