# Query BizTalk Rules XML file

param
(
	$xmlrulesfile=$(throw 'Must provide XML rules file'),
	$env=$(throw 'Must provide env: iDev,iTest,Stage,Prod'),
    [string] $style,
    [string] $all
)

$xml = $null	# in case we have it set.  Learn how to do scope

$xml = [xml] (get-content $xmlrulesfile)

# Show what is in file
$xml.Brl.xmlns

$Vocabularies = ($xml.Brl.vocabulary | sort -Property name)

$RuleSets = ($xml.Brl.ruleset | sort -Property name)

# Display Info about Vocabularies

""
"********************"
"Vocabularies"
"********************"
""

foreach ($v in $Vocabularies)
{
	$v.Name
	$v.version | format-list
}

# Display Info about RuleSets

""
"********************"
"RuleSets"
"********************"
""

$lastRuleSet = $RuleSets[0]

foreach ($ruleset in $RuleSets)
{
	# "Debug " + $lastRuleSet.name + ":" + $ruleset.name
	
	# if ($lastRuleSet.name -eq $ruleset.name)
	# {
		# "Debug - Matching names"
		# "Debug " + $ruleset.version.major + ":" + $lastRuleSet.version.major
		
		# if($ruleset.version.major -lt $lastRuleSet.version.major)
		# {
			# "Debug Lower Major Version, skipping"
		# }
		# else
		# {
			# "Debug " + $ruleset.version.minor + ":" + $lastRuleSet.version.minor
			# if ($ruleset.version.minor -lt $lastRuleSet.version.minor)
			# {
				# "Lower Minor Verison, skipping"
			# }
		# }
	# }
	
	"ruleset.name: " + $ruleset.name
	"ruleset.configuration: " + $ruleset.configuration
	# version has
	# 	major
	# 	minor
	# 	description
	# 	modifiedby
	# 	date	
	"ruleset.version: " + $ruleset.version.major `
		+ "." + $ruleset.version.minor `
		+ " " + $ruleset.version.description `
		+ " " + $ruleset.version.modifiedby `
		+ " " + $ruleset.version.date
	
	# rule seems to have the interesting stuff
	
	foreach ($rule in $ruleset.rule)
	{
		"  rule.name: " + $rule.name + "  active: " + $rule.active
		
		foreach ($function in $rule.then.function)
		{
			if (($function.classmember.argument).Count -eq 2)
			{
				# This seems to be the case for simple key/value pairs
				
				$key = $function.classmember.argument[0].constant.string
				$value = $function.classmember.argument[1].constant.string
				
				"    " + $key + " ->" + $value + "<-"
			}
			else
			{
				foreach($a in $function.classmember.argument)
				{
					"    " + $a.constant.string
				}
			}		
		}
	}
	
	""
	$lastRuleSet = $ruleset
}

exit



