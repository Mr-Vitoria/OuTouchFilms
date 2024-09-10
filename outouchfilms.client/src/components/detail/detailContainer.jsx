import React, { Component } from "react";
import { geFranchiseAnimeList, getAnime } from "../../api/animeService";
import { Slogan } from "./models/slogan";

import style from "../../assets/css/detail.module.scss";
import { Player } from "./models/player";
import { AnimeInfo } from "./models/animeInfo";
import { TrailerList } from "./models/trailerList";
import { ScreenshotList } from "./models/screenshotList";
import { ReviewList } from "./models/reviewList";
import { ScreenshotModal } from "./models/screenshotModal";
import { getCommentList } from "../../api/commentService";
import { PageLoader } from "../shared/pageLoader";
import { getCookie } from "../../functions/cookie";
import { FranchiseList } from "./models/franchiseList";

export class DetailContainer extends Component {

    constructor(props) {
        super(props);
        this.state = {
            animeId: new URLSearchParams(window.location.search).get('id'),
            anime: null,
            commentList: null,
            franchiseList: null,
            userId: getCookie("userId")
        }

        this.screenshotModal = React.createRef();
        this.pageLoaderRef = React.createRef();

        this.getAnimeEvent = this.getAnimeEvent.bind(this);
        this.getCommentEvent = this.getCommentEvent.bind(this);
        this.getFranchiseListEvent = this.getFranchiseListEvent.bind(this);

    }

    componentDidMount() {
        this.getAnimeEvent();
        this.getCommentEvent();
    }

    async getAnimeEvent() {
        const result = await getAnime(this.state.animeId, this.state.userId ?? -1);

        if (result != false) {
            this.setState({
                anime: result
            });
            this.pageLoaderRef.current.close();
            this.getFranchiseListEvent(result.franchise);
        }
    }

    async getCommentEvent() {
        const result = await getCommentList(this.state.animeId);

        if (result != false) {
            this.setState({
                commentList: result
            });
        }
    }

    async getFranchiseListEvent(franchise) {

        if (franchise == "") {
            return;
        }
        const result = await geFranchiseAnimeList(franchise);

        if (result != false) {
            this.setState({
                franchiseList: result.animeList
            });
        }
    }

    render() {
        return <>
            {
                this.state.anime != null ?
                    <>
                        <ScreenshotModal ref={this.screenshotModal} />

                        <Slogan anime={this.state.anime} userId={this.state.userId}/>
                        <div className={`${style.blur}`}></div>
                        <Player anime={this.state.anime} userId={this.state.userId} />
                        <AnimeInfo anime={this.state.anime} />
                        <TrailerList trailers={this.state.anime.videos} />
                        <ScreenshotList screenshots={this.state.anime.screenshots} onClick={(imgUrl) => {
                            this.screenshotModal.current.open(imgUrl);
                        }} />

                        {
                            this.state.commentList != null ?
                                <ReviewList reviews={this.state.commentList} />
                                : null
                        }

                        {
                            this.state.franchiseList != null ?
                                <FranchiseList watchAnimeId={this.state.animeId} franchiseList={this.state.franchiseList} />
                                : null
                        }
                    </>
                    :
                    <p>Загрузка</p>
            }

            <PageLoader ref={this.pageLoaderRef} />
        </>
    }
}