import React, { Component } from "react";

import { AnimeCarousel } from "./animeCarousel";
import { RecommendAnimeSection } from "./recommendAnimeSection";
import { getListCurrentSeason } from "../../api/animeService";
import { PageLoader } from "../shared/pageLoader";
import { SheduleSection } from "./sheduleSection";

export class IndexContainer extends Component {

    constructor(props) {
        super(props);

        this.state = {
            animeList: null,
            genreList: null,
            needLoad: true,
            
        };

        this.getAnimeListEvent = this.getAnimeListEvent.bind(this);

        this.pageLoaderRef = React.createRef();
    }

    componentDidMount() {
        this.getAnimeListEvent();
    }

    async getAnimeListEvent() {
        const animeList = await getListCurrentSeason();

        if (animeList != false) {
            this.setState({
                animeList: animeList
            });
            this.pageLoaderRef.current.close();
        }
    }

    render() {
        return <>
            {
                this.state.animeList != null ? 
                <AnimeCarousel animeList={this.state.animeList} />
                : null
            }
            
            <SheduleSection />
            <RecommendAnimeSection />

            <PageLoader ref={this.pageLoaderRef} />
        </>
    }
}