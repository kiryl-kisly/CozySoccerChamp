import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import { IPredictionResponse } from '../../services/interfaces/Responses/IPredictionResponse'

export interface PredictionsState {
	predictions: IPredictionResponse[] | null
}

const initialState: PredictionsState = {
	predictions: null,
}

const predictionsSlice = createSlice({
	name: 'predictions',
	initialState,
	reducers: {
		setPredictions(state, action: PayloadAction<IPredictionResponse[]>) {
			state.predictions = action.payload
		},
		updatePrediction(state, action: PayloadAction<IPredictionResponse>) {
			if (state.predictions) {
				const index = state.predictions.findIndex(
					(pred) => pred.matchId === action.payload.matchId
				)
				if (index !== -1) {
					state.predictions[index] = action.payload
				} else {
					state.predictions.push(action.payload)
				}
			} else {
				state.predictions = [action.payload]
			}
		},
	},
})

export const { setPredictions, updatePrediction } = predictionsSlice.actions
export default predictionsSlice.reducer