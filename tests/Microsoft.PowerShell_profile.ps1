
Import-Module posh-git




#     ▶ ➤ » ᐅ ❯




# Default prompt layout:
# {DPPrefix}{DPPath}{PathStatusSeparator}<{BeforeStatus}{Status}{AfterStatus}>{DPMiddle}<{DPDebug}><{DPTimingFormat}>{DPSuffix}




# Prompt layout when DefaultPromptWriteStatusFirst is set to $true:
# {DPPrefix}<{BeforeStatus}{Status}{AfterStatus}>{PathStatusSeparator}{DPPath}{DPMiddle}<{DPDebug}><{DPTimingFormat}>{DPSuffix}




$GitPromptSettings.DefaultPromptAbbreviateHomeDirectory = $false
# $GitPromptSettings.DefaultPromptEnableTiming = $true








$SegmentBg = 'DimGray'
$SegmentFg = 'AliceBlue'




# $GitPromptSettings.DefaultPromptPrefix.Text = '`n$(Get-Date -f "HH:mm:ss") '
$GitPromptSettings.DefaultPromptPrefix.Text = '`n'


# This is not working )
# $GitPromptSettings.DefaultPromptPrefix.Text = '`n'
# $GitPromptSettings.DefaultPromptPrefix.BackgroundColor = $SegmentBg
# $GitPromptSettings.DefaultPromptPrefix.ForegroundColor = 'OrangeRed'


$GitPromptSettings.DefaultPromptPath.Text = ' $(Get-PromptPath) '
$GitPromptSettings.DefaultPromptPath.ForegroundColor = $SegmentFg
$GitPromptSettings.DefaultPromptPath.BackgroundColor = $SegmentBg




$GitPromptSettings.PathStatusSeparator.Text = " "
$GitPromptSettings.PathStatusSeparator.ForegroundColor = $SegmentFg
$GitPromptSettings.PathStatusSeparator.BackgroundColor = $SegmentBg




$GitPromptSettings.DelimStatus.BackgroundColor = $SegmentBg
$GitPromptSettings.BeforeStash.BackgroundColor = $SegmentBg
$GitPromptSettings.AfterStash.BackgroundColor = $SegmentBg
$GitPromptSettings.BranchColor.BackgroundColor = $SegmentBg
$GitPromptSettings.BranchColor.BackgroundColor = $SegmentBg




$GitPromptSettings.IndexColor.BackgroundColor = $SegmentBg
$GitPromptSettings.WorkingColor.BackgroundColor = $SegmentBg
$GitPromptSettings.StashColor.BackgroundColor = $SegmentBg
$GitPromptSettings.ErrorColor.BackgroundColor = $SegmentBg




$GitPromptSettings.BeforeStatus.Text = ' '
$GitPromptSettings.BeforeStatus.BackgroundColor = $SegmentBg
$GitPromptSettings.AfterStatus.Text = ' '
$GitPromptSettings.AfterStatus.BackgroundColor = $SegmentBg
$GitPromptSettings.LocalDefaultStatusSymbol.Text = '✔'
$GitPromptSettings.LocalDefaultStatusSymbol.BackgroundColor = $SegmentBg
$GitPromptSettings.LocalWorkingStatusSymbol.Text = "✎"
$GitPromptSettings.LocalWorkingStatusSymbol.BackgroundColor = $SegmentBg
$GitPromptSettings.LocalStagedStatusSymbol.Text = '✱'
$GitPromptSettings.LocalStagedStatusSymbol.BackgroundColor = $SegmentBg
$GitPromptSettings.BranchGoneStatusSymbol.Text = '✘'
$GitPromptSettings.BranchGoneStatusSymbol.BackgroundColor = $SegmentBg


$GitPromptSettings.DefaultPromptBeforeSuffix.Text = ' `n'
$GitPromptSettings.DefaultPromptBeforeSuffix.ForegroundColor = $SegmentBg


# $GitPromptSettings.DefaultPromptTimingFormat.Text = '{0}ms '
$GitPromptSettings.DefaultPromptSuffix.Text = '$("$" * ($nestedPromptLevel + 1)) '
$GitPromptSettings.DefaultPromptSuffix.ForegroundColor = 'Green'








# ## Displaying file status in the git prompt for a very large repo can be prohibitively slow.
# ## Rather than turn off file status entirely ($GitPromptSettings.EnableFileStatus = $false),
# ## you can disable it on a repo-by-repo basis by adding individual repository paths
# ## to $GitPromptSettings.RepositoriesInWhichToDisableFileStatus.
$GitPromptSettings.EnableFileStatus = $false
$GitPromptSettings.LocalDefaultStatusSymbol.Text = ''




# # Chocolatey profile
# $ChocolateyProfile = "$env:ChocolateyInstall\helpers\chocolateyProfile.psm1"
# if (Test-Path($ChocolateyProfile)) {
# Import-Module "$ChocolateyProfile"
# }
