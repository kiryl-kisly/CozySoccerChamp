import { useEffect, useState } from 'react'
import { Competition } from '../../components/Competition/Competition'
import { ICompetitionResponse } from '../../services/interfaces/Responses/ICompetitionResponse'
import { ILeaderboardResponse } from '../../services/interfaces/Responses/ILeaderboardResponse'
import { getLeaderboard } from '../../services/LeaderboardService'
import './LeaderboardPage.css'

interface Props {
	competition: ICompetitionResponse | null
	leaderboard: ILeaderboardResponse[] | null
}

export function LeaderboardPage({ leaderboard, competition }: Props) {
	const [data, setData] = useState<ILeaderboardResponse[] | null>(leaderboard)

	useEffect(() => {
		async function fetchData() {
			setData((await getLeaderboard()))
		}
		fetchData()
	}, [])

	return (
		<>
			<h1 className='title-page'>Leaderboard</h1>

			<Competition competition={competition} />

			{data && data.length > 0 ? (
				data.map((item: ILeaderboardResponse, index: number) => (
					<div key={index} className='leaderboard-item relative flex justify-between items-end border-b border-gray-500 mb-5'>
						<div className='absolute left-0 text-green-500 p-2'>#{item.place}</div>
						<div className='ml-[40px] text-white p-2 font-normal'>{item.userName}</div>
						<div className='ml-auto text-green-300 p-2'>{item.points}</div>
					</div>
				))
			) : (
				<p className='text-white'>Data for the leaderboard is not yet available</p>
			)}
		</>
	)
}