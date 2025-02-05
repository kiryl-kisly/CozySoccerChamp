import { createSlice, PayloadAction } from '@reduxjs/toolkit'

export interface UserProfileState {
	isEnabledNotification: boolean
}

const initialState: UserProfileState = {
	isEnabledNotification: true,
}

const userProfileSlice = createSlice({
	name: 'userProfile',
	initialState,
	reducers: {
		setEnabledNotification: (state, action: PayloadAction<boolean>) => {
			state.isEnabledNotification = action.payload
		},
	},
})

export const { setEnabledNotification } = userProfileSlice.actions
export default userProfileSlice.reducer
