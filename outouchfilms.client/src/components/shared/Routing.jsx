import {
    BrowserRouter as Router,
    Routes,
    Route
} from "react-router-dom";

import { Component } from "react";
import { IndexContainer } from "../index/IndexContainer";
import { DetailContainer } from "../detail/detailContainer";
import { LoginContainer } from "../login/loginContainer";
import { ProfileContainer } from "../profile/profileContainer";
import { SearchContainer } from "../search/searchContainer";
import { SheduleContainer } from "../shedule/sheduleContainer";

export default class Routing extends Component {

    constructor(props) {
        super(props);

    }

    render() {
        return <Router>
            <Routes>
                <Route path="/" element={<IndexContainer />} />
                <Route path="/detail" element={<DetailContainer />} />
                <Route path="/login" element={<LoginContainer />} />
                <Route path="/profile" element={<ProfileContainer />} />
                <Route path="/search" element={<SearchContainer />} />
                <Route path="/shedule" element={<SheduleContainer />} />
            </Routes>
        </Router>
    }
}